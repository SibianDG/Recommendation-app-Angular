describe("Add new Recommendation Test", function () {

  beforeEach(() => {
    cy.server();

    cy.visit("http://localhost:4200/add-recommendation");
    cy.get("[data-cy=login]").click();
    cy.get('[data-cy=login-email]').type('sibian.dg@student.hogent.be');
    cy.get('[data-cy=login-password]').type('P@ssword1111');
    cy.get("[data-cy=login-button]").click();
  });
  

  it("add new recommendation error", function () {
    cy.route({
      method: "POST",
      url: "/api/Recommendation",
      status: 500,
      response: 'Error',
    });
    cy.visit("http://localhost:4200/add-recommendation");
    cy.get('[data-cy=selectedTypeAdd]').click().get('mat-option').contains('Book').click();
    cy.get('[data-cy=titleAdd]').type('book123');
    cy.get('[data-cy=summaryAdd]').type('summary123450');
    cy.get("[data-cy=submitAdd]").click();
    cy.get("[data-cy=btnAddFullRecommendation]").click();
    cy.get("[data-cy=addThisRecommendationError]").should('be.visible');
  });

  it("SERVER TEST: fetch url", function () {

    cy.visit("http://localhost:4200/add-recommendation");
    
    cy.get('[data-cy=selectedTypeAdd]').click().get('mat-option').contains('Book').click();
    cy.get('[data-cy=url]').type('https://www.standaardboekhandel.be/p/stemmen-aan-het-front-9789026150982');
    cy.get("[data-cy=fetchURLButton]").click();

    //TODO wait
    cy.wait(5000);

    cy.get("[data-cy=titleAdd]").should('have.value', 'Stemmen aan het front');
    cy.get('[data-cy=summaryAdd]').invoke('val').should('contain', 'December 1917. Terwijl Europa in brand staat, wordt in Philadelphia de 24-jarige Ruby Wagner geacht zich uitsluitend te richten op haar aankomende societyhuwelijk. Maar Ruby besluit zich aan te melden bij de verbindingsdienst van het Amerikaanse leger – de vrouwen die aan het front de telefoonlijnen bedienen. Als vrouwen in het leger worden zij en haar collega’s continu gekleineerd en onderschat, maar hun werk is van levensbelang.');
    cy.get("[data-cy=imageAdd]").should('have.value', 'https://media.standaardboekhandel.be/-/media/mdm/product/9789026150982/frontImagesLink.img?rev=926863092&w=525&hash=4B93388AFF556F7EC60C2A4D31E38DCF');
    cy.get("[data-cy=pagesAdd]").should('be.visible');
    cy.get("[data-cy=pagesAdd]").should('have.value', '384');
        
  });
});
