// describe('register succesfull', () => {
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

describe('register failed', () => {
  it('fails', () => {
    cy.visit('/register')

    cy.get('#username').type('pietza');
    cy.get('#email').type('E2eTestacc@example.com');
    cy.get('#pass').type('user123');
    cy.get('#repass').type('user123');

    cy.get('#submit').click({force: true});

    cy.contains('div', 'account not created, username or password already taken').should('be.visible');
  })
})