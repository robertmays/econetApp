import { Photo } from './photo';

export interface Employee {
    id: number;
    username: string;
    photoUrl: string;
    knownAs: string;
    created: Date;
    lastActive: Date;
    age: number;
    gender: string;
    title: string;
    initials: string;
    lastname: string;
    photos: Photo[];
  }
  
