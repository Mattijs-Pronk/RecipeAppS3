describe('Resetpassword failed', () => {
  it('fails', () => {
    cy.visit('/reset')

    cy.get('#token').type('123123123');
    cy.get('#pass').type('petertje123');
    cy.get('#repass').type('petertje123');

    cy.get('#submit').click({force: true});

    cy.contains('div', 'token is incorrect or already used').should('be.visible');
  })
})