describe("Details tests", function () {
  
  beforeEach(() => {
    cy.server();

    cy.visit("http://localhost:4200/add-recommendation");
    cy.get("[data-cy=login]").click();
    cy.get('[data-cy=login-email]').type('sibian.dg@student.hogent.be');
    cy.get('[data-cy=login-password]').type('P@ssword1111');
    cy.get("[data-cy=login-button]").click();
  });

  it("button disabled to edit when server not reachable", function () {
    cy.route({
      method: "GET",
      url: "/api/Items/298",
      status: 500,
      response: 'Error',
    });
    cy.visit("http://localhost:4200/details/298");
    cy.get("[data-cy=edit]").should('be.disabled');
  });

  it("shows error message", function () {
    cy.route({
      method: "GET",
      url: "/api/Items/298",
      status: 500,
      response: 'Error',
    });
    cy.visit("http://localhost:4200/details/298");
    cy.get("[data-cy=errorFullDetail]").should('be.visible');
  });

  it("edit details item", function () {
    cy.route({
      method: "GET",
      url: "/api/Items/298",
      status: 200,
      response: 'fixture:randomRecommendationItem.json',
    });
    cy.visit("http://localhost:4200/details/298");
    cy.get("[data-cy=edit]").click();
    cy.get("[data-cy=app-edit-item]").should('be.visible');

    const randomRecommendationItem = require('../../fixtures/randomRecommendationItem.json');
    cy.get("[data-cy=title]").contains(randomRecommendationItem.title);
    cy.get("[data-cy=typeName]").contains(randomRecommendationItem.typeName);
    cy.get("[data-cy=summary]").contains(randomRecommendationItem.summary);
    if (randomRecommendationItem.typeName == "Book" && randomRecommendationItem.pages > 0){
      cy.get("[data-cy=page]").should('be.visible');
      cy.get("[data-cy=page]").contains(randomRecommendationItem.pages);
    }
    if (randomRecommendationItem.typeName == "Movie" && randomRecommendationItem.duration > 0){
      cy.get("[data-cy=duration]").should('be.visible');
      cy.get("[data-cy=duration]").contains(randomRecommendationItem.duration);
    }
    if (randomRecommendationItem.typeName == "Serie" && randomRecommendationItem.numberOfEpisodes > 0){
      cy.get("[data-cy=numberOfEpisodes]").should('be.visible');
      cy.get("[data-cy=numberOfEpisodes]").contains(randomRecommendationItem.numberOfEpisodes);
    }
  });

});
