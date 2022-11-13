describe('EditProfile succesful', () => {
  it('succes', () => {
    cy.visit('/myprofile')

    localStorage.setItem('user', 26);

    cy.get('#adress').type('piet str.15');
    cy.get('#phone').type('0494930323');

    cy.get('#submitProfile').click({force: true});

    cy.contains('div', 'Succesfully changed profile').should('be.visible');
  })
})