import { describe, expect, test } from 'vitest'
import mockAxios from "axios";
//import API from "./API"

// Run all test: npm run test:unit

//Unit test for signing off
import { Logout } from '../../assets/Functions/Auth'
describe('Logout', () => {
  test('Deletes the localStorage item properly.', () => {
    localStorage.setItem('user', JSON.stringify("Unit-Test"))
    expect(Logout()).toBe(true)
  })
})

describe('Logout', () => {
  test('Can not find user to logout.', () => {
    expect(Logout()).toBe(false)
  })
})



//Unit Test RouteGaurd
import { RouteGaurd } from '../../assets/Functions/Auth'
describe('RouteGaurd', () => {
  test('User is found', () => {
    localStorage.setItem('user', JSON.stringify("Unit-Test-New"))
    expect(RouteGaurd()).toBe(true)
    localStorage.removeItem('user')
  })
})

describe('RouteGaurd', () => {
  test('User is not found', () => { expect(RouteGaurd()).toBe(false) })
})




//Unit Test Login
// import { Login } from '../../assets/Functions/Auth'
// describe('Login', () => {
//   test('User sign in', async () => {
//     const user = [
//       { id: 1, email: "John@example.com", password: "john123" },
//     ];
//     mockAxios.get.mockResolvedValueOnce(user);

//     expect(await Login('John@example.com', 'john123')).toBe(true)
//   })
// })
