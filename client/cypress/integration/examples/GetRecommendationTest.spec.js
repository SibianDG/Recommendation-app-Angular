describe("Get Recommendation Test", function () {

  beforeEach(() => {
    cy.server();

    cy.visit("http://localhost:4200/add-recommendation");
    cy.get("[data-cy=login]").click();
    cy.get('[data-cy=login-email]').type('sibian.dg@student.hogent.be');
    cy.get('[data-cy=login-password]').type('P@ssword1111');
    cy.get("[data-cy=login-button]").click();
  });

  it("mock RecommendationItem get", function () {
    cy.route({
      method: "GET",
      url: "/api/Items/getrecommendationitem",
      status: 200,
      response: 'fixture:randomRecommendationItem.json',
    });
    cy.visit("http://localhost:4200/");
    cy.get("[data-cy=btnGetRecommendation]").click();
    cy.get("[data-cy=itemCard]").should("have.length", 1);
  });

  it("remove RecommendationItem", function () {
    cy.route({
      method: "GET",
      url: "/api/Items/getrecommendationitem",
      status: 200,
      response: 'fixture:randomRecommendationItem.json',
    });
    cy.visit("http://localhost:4200/");
    cy.get("[data-cy=btnGetRecommendation]").click();
    cy.get("[data-cy=itemCard]").should("have.length", 1);
    cy.get("[data-cy=removeItem]").click();
    cy.get("[data-cy=itemCard]").should("have.length", 0);
  });

  it("go to details of RecommendationItem", function () {
    cy.route({
      method: "GET",
      url: "/api/Items/getrecommendationitem",
      status: 200,
      response: 'fixture:randomRecommendationItem.json',
    });
    cy.visit("http://localhost:4200/");
    cy.get("[data-cy=btnGetRecommendation]").click();
    
    cy.url().then(url => {
      cy.get("[data-cy=infoRecommendation]").click();
      cy.url().should('not.eq', url);
      cy.url().should('include', '/details/298');
    });

  });
});
