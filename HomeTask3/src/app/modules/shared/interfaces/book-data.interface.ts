import { IBookReview } from './book-review.interface';

export interface IBookData {
  title: string;
  cover: string;
  content: string;
  author: string;
  genre: string;
  reviews: IBookReview[];
  rating: number;
}
