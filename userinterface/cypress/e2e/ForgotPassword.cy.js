describe('Forgotpassword failed', () => {
  it('fails', () => {
    cy.visit('/forgot')

    cy.get('#email').type('aiskdjaojisd@gmail.com');

    cy.get('#submit').click({force: true});

    cy.contains('div', 'email was not found').should('be.visible');
  })
})