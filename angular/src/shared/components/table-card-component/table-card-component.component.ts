import { Component, Input, SimpleChanges} from '@angular/core';

@Component({
  selector: 'table-card-component',
  templateUrl: './table-card-component.component.html',
  styleUrl: './table-card-component.component.css'
})
export class TableCardComponentComponent  {
  @Input() data: any[] = [];
  @Input() columns: { field: string, header: string }[] = [];
  @Input() totalItems: number = 0;
  @Input() pageSize: number = 10;
  @Input() currentPage: number = 1;
  @Input() baseRoute?: string | null;

  @Input() loadPage: (page: number) => void;

  hasMoreItems: boolean = false;

  ngOnInit(): void {
    this.calculatePagination();
  }

  ngOnChanges(changes: SimpleChanges): void {
    if (changes.totalItems || changes.currentPage) {
      this.calculatePagination();
    }
  }

  private calculatePagination(): void {
    this.hasMoreItems = (this.currentPage * this.pageSize) < this.totalItems;
  }

  nextPage(): void {
    if (this.hasMoreItems) {
      this.currentPage++;
      this.loadPage(this.currentPage);
    }
  }

  previousPage(): void {
    if (this.currentPage > 1) {
      this.currentPage--;
      this.loadPage(this.currentPage);
    }
  }

  groupColumns(array: any[], groupSize: number): any[][] {
    const groupedArray = [];
    for (let i = 0; i < array.length; i += groupSize) {
      groupedArray.push(array.slice(i, i + groupSize));
    }
    return groupedArray;
  }
}
