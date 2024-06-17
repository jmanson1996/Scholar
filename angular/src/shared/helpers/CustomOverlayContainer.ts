import { OverlayContainer } from '@angular/cdk/overlay';

export class CustomOverlayContainer extends OverlayContainer {
  _createContainer(): void {
    const container = document.createElement('div');
    container.classList.add('cdk-overlay-container');

    const parentElement = document.querySelector('.modal') || document.body;
    parentElement.appendChild(container);
    this._containerElement = container;
  }
}
