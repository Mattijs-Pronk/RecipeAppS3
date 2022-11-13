import 'cypress-file-upload';

describe('AddRecipe failed', () => {
  it('fails', () => {
    localStorage.setItem('user', 0)

    cy.visit('/addrecipe')

    cy.get('#title').type('Burger');
    cy.get('#preptime').type('25');
    cy.get('#portions').type('2');
    cy.get('#ingredients').type('ham, brood');
    cy.get('#description').type('bak in pan');
    cy.fixture('profilePic.png').then(fileContent => {
      cy.get('input[type="file"]').attachFile({
          fileContent: fileContent.toString(),
          fileName: 'profilePice.png',
          mimeType: 'image/png'
      });
  });

    cy.get('#submit').click({force: true});

    cy.contains('div', 'recipe has not been send for approval, something went wrong').should('be.visible');
  })
})