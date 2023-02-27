import { IBookReview } from './book-review.interface';

export interface IBookReviewsData {
  id: number;
  title: string;
  cover: string;
  content: string;
  author: string;
  genre: string;
  reviews: IBookReview[];
  rating: number;
}
