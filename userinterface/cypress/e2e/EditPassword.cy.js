describe('EditPassword succesful', () => {
  it('succes', () => {
    cy.visit('/myprofile')

    localStorage.setItem('user', 26);

    cy.get('#currentpass').type('user123');
    cy.get('#pass').type('user123');
    cy.get('#repass').type('user123');

    cy.get('#submitPass').click({force: true});

    cy.contains('div', 'Succesfully changed password').should('be.visible');
  })
})

describe('EditPassword failed', () => {
  it('fails', () => {
    cy.visit('/myprofile')

    localStorage.setItem('user', 26);

    cy.get('#currentpass').type('user123456');
    cy.get('#pass').type('user123');
    cy.get('#repass').type('user123');

    cy.get('#submitPass').click({force: true});

    cy.contains('div', 'incorrect password').should('be.visible');
  })
})