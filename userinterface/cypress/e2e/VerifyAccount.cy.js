describe('verify failed', () => {
  it('fails', () => {
    cy.visit('/verify')

    cy.get('#token').type('123123213');
    cy.get('#email').type('E2eTestacc@example.com');

    cy.get('#submit').click({force: true});

    cy.contains('div', 'token is incorrect or already used', {force: true}).should('be.visible');
  })
})