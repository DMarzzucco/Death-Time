## Discription

This project implements a technique inspired by the idea of ​​pressuring customers to make outstanding payments by progressively rendering the application unusable after a specific deadline.

## Functionality

The core logic of the technique is based on server-side middleware that checks the current date against a predefined deadline. Once this deadline has passed, the application blocks all critical operations and deletes records from the database, ensuring that the application can no longer be used.

```TS
/* change these variables as need */
const currenTime = new Date();
const deathTime = new Date('YYYY-MM-DDTHH:mm:ssZ') //Set the deadline here
/* stop changing here */
```

## Author

Inspired from (@kleampa)
Made by Dario Marzzucco (@darmarzz)

## Advertising

This repository is designed for educational and demonstration purposes. The legality of its use in real applications is not guaranteed. The author is not responsible for improper use of this technique.
