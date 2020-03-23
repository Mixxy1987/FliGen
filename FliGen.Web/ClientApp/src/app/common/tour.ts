import { TourStatus } from "./tourStatus";

export class Tour {
  id: number;
  date: string;
  homeCount: number;
  guestCount: number;
  tourStatus: TourStatus;
}
