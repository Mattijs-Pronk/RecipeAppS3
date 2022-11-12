//cypress openen: npx cypress open

describe('empty spec', () => {
  it('passes', () => {
    cy.visit('/login')

    cy.get('#email').type('cloudrecipes.info@gmail.com');
    cy.get('#pass').type('admin123');

    cy.get('#submit').click();

    cy.contains('div', 'Succesfully logged in').should('be.visible');
  })
})

describe('empty spec', () => {
  it('passes', () => {
    cy.visit('/login')

    cy.get('#email').type('test@example.com');
    cy.get('#pass').type('testpass');

    cy.get('#submit').click();

    cy.contains('div', 'password or email did not match or account is not activated').should('be.visible');
  })
})