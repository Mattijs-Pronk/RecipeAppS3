describe('login succesful', () => {
  it('passes', () => {
    cy.visit('/login')

    cy.get('#email').type('E2eTestacc@example.com');
    cy.get('#pass').type('user123');

    cy.get('#submit').click({force: true});

    cy.contains('div', 'Succesfully logged in').should('be.visible');
  })
})

describe('login failed', () => {
  it('fails', () => {
    cy.visit('/login')

    cy.get('#email').type('asdasdasdasdasdasd@example.com');
    cy.get('#pass').type('testpass');

    cy.get('#submit').click({force: true});

    cy.contains('div', 'password or email did not match or account is not activated').should('be.visible');
  })
})