// describe('empty spec', () => {
//   it('passes', () => {
//     cy.visit('/register')

//     cy.get('#username').type('janenalleman');
//     cy.get('#email').type('petflet5@example.com');
//     cy.get('#pass').type('user123');
//     cy.get('#pass').type('user123');

//     cy.get('#submit').click();

//     cy.contains('div', 'account has been created, check your email to verify your account').should('be.visible');
//   })
// })

describe('empty spec', () => {
  it('passes', () => {
    cy.visit('/register')

    cy.get('#username').type('CloudRecipes');
    cy.get('#email').type('cloudrecipes.info@gmail.com');
    cy.get('#pass').type('user123');
    cy.get('#repass').type('user123');

    cy.get('#submit').click();

    cy.contains('div', 'account not created, username or password already taken').should('be.visible');
  })
})