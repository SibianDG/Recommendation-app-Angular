describe("Authentication Test", function () {


    it("login", function () {
        cy.server();
        cy.visit("http://localhost:4200/add-recommendation");
        cy.get("[data-cy=login]").click();
        cy.get('[data-cy=login-email]').type('sibian.dg@student.hogent.be');
        cy.get('[data-cy=login-password]').type('P@ssword1111');
        cy.get("[data-cy=login-button]").click();
    })
})