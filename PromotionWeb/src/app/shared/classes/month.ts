export class Month {
  date: Date;
  name: string;
  number: number;
  year: number;

  constructor(date: Date) {
    this.date = date;
    this.setFields();
  }

  private setFields() {
    this.number = this.date.getMonth();
    this.name = this.date.toLocaleString('ru', { month: 'long' });
    this.year = this.date.getFullYear();
  }

  monthBack() {
    this.date.setMonth(this.date.getMonth() - 1);
    this.setFields();
  }

  monthForward() {
    this.date.setMonth(this.date.getMonth() + 1);
    this.setFields();
  }
}
