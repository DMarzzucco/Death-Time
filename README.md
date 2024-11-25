# Death Time

This project implements a technique inspired by the idea of ​​pressuring customers to make outstanding payments by progressively rendering the application unusable after a specific deadline.

## Functionality

The core logic of the technique is based on server-side middleware that checks the current date against a predefined deadline. Once this deadline has passed, the application blocks all critical operations and deletes records from the database, ensuring that the application can no longer be used.

## Implementation
```
Death Time = ('YEAR-MONTH-DAY HOURS:MINUTE:SECOND') 
```
## Exapmples 
```TS
const currenTime = new Date();
const deathTime = new Date('YYYY-MM-DDTHH:mm:ssZ') 
```
```PY
current_time = datetime.utcnow()
death_time = datetime.strptime('YYYY-MM-DDTHH:mm:ssZ', '%Y-%m-%dT%H:%M:%SZ')
```
```CS
var currentTime = DateTime.UtcNow;
var deathTime = DateTime.Parse("YYYY-MM-DDTHH:mm:ssZ"); 
```
## Examples in Project

If you wanna see how work this middleware. You can test [here](https://github.com/DMarzzucco/register_Date_with_No_Paid_incluid.git)

## APIs

[ASP.NET](https://github.com/DMarzzucco/DeathTime.ASP.NET.git)

[ExpressJs](https://github.com/DMarzzucco/DeathTime.Express-API.git)

[SpringBoot](https://github.com/DMarzzucco/DeathTimewithSpringBoot)

## Author

Inspired from (@kleampa)
Made by Dario Marzzucco (@darmarzz)

## Advertising

This repository is designed for educational and demonstration purposes. The legality of its use in real applications is not guaranteed. The author is not responsible for improper use of this technique.
