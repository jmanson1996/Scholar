import { Component, Input } from '@angular/core';
import jsPDF from 'jspdf';
import html2canvas from 'html2canvas';

@Component({
  selector: 'app-pdf-exporter',
  templateUrl: './pdf-exporter.component.html',
  styleUrls: ['./pdf-exporter.component.css']
})
export class PdfExporterComponent {
  @Input() tableId: string;
  @Input() omitColumn: string;
  @Input() nameFile : string;

  constructor() {}

  exportToPDF() {
    const data = document.getElementById(this.tableId);

    if (data) {
      this.hideColumn(data, this.omitColumn);
      html2canvas(data).then(canvas => {
        const imgWidth = 297 - 20; // A4 width in mm, with margin
        const pageHeight = 210 - 20; // A4 height in mm, with margin
        const imgHeight = canvas.height * imgWidth / canvas.width;
        let heightLeft = imgHeight;
        const margin = 10;
        let position = 0;

        const pdf = new jsPDF('l', 'mm', 'a4'); // 'l' for landscape

        pdf.addImage(canvas.toDataURL('image/png'), 'PNG', margin, position + margin, imgWidth, imgHeight);
        heightLeft -= pageHeight;

        while (heightLeft > 0) {
          position = heightLeft - imgHeight;
          pdf.addPage();
          pdf.addImage(canvas.toDataURL('image/png'), 'PNG', margin, position + margin, imgWidth, imgHeight);
          heightLeft -= pageHeight;
        }
        let fecha = new Date();
        pdf.save(this.nameFile +"_"+ this.setDate() +'.pdf');
        this.showColumn(data, this.omitColumn);
      });
    }
  }

  hideColumn(table: HTMLElement, columnName: string) {
    const headers = table.querySelectorAll('th');
    let columnIndex = -1;

    headers.forEach((header, index) => {
      if (header.textContent?.trim() === columnName) {
        columnIndex = index;
        (header as HTMLElement).style.display = 'none';
      }
    });

    if (columnIndex !== -1) {
      const rows = table.querySelectorAll('tr');
      rows.forEach(row => {
        const cells = row.querySelectorAll('td, th');
        if (cells[columnIndex]) {
          (cells[columnIndex] as HTMLElement).style.display = 'none';
        }
      });
    }
  }

  showColumn(table: HTMLElement, columnName: string) {
    const headers = table.querySelectorAll('th');
    let columnIndex = -1;

    headers.forEach((header, index) => {
      if (header.textContent?.trim() === columnName) {
        columnIndex = index;
        (header as HTMLElement).style.display = '';
      }
    });

    if (columnIndex !== -1) {
      const rows = table.querySelectorAll('tr');
      rows.forEach(row => {
        const cells = row.querySelectorAll('td, th');
        if (cells[columnIndex]) {
          (cells[columnIndex] as HTMLElement).style.display = '';
        }
      });
    }
  }

  setDate() : string{
    const dateCurrent = new Date();
    const day = dateCurrent.getDate();
    const month = dateCurrent.getMonth() + 1;
    const year = dateCurrent.getFullYear();
    const dateFormatted = `${day}/${month}/${year}`;
    return dateFormatted;
}
}
