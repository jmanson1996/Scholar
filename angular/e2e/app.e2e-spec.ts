import { ScholaTemplatePage } from './app.po';

describe('Schola App', function() {
  let page: ScholaTemplatePage;

  beforeEach(() => {
    page = new ScholaTemplatePage();
  });

  it('should display message saying app works', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('app works!');
  });
});
