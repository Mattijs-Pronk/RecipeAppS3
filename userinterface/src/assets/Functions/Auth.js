import APIcalls from "./services/APIcalls";
import { HashPassword } from "./PasswordHash";

export const Login = async (email, password) => {
  try{
    let response = await APIcalls.Login(email, password)

    localStorage.setItem('user', JSON.stringify((response).data))
    return true
  }
  catch{
    return false
  } 
}

export const Register = async (username, email, password) => {
  const isadmin = false;
  const passwordhash = await HashPassword(password);

  await APIcalls.Register(username, passwordhash, email, isadmin)
}

export const Logout = async () => {
  localStorage.clear();
}

export const CheckUser = async (username) => {
  let response = await APIcalls.CheckUsername(username)
  return response.data
}

export const CheckEmail = async (email) => {
  let response = await APIcalls.CheckEmail(email)
  return response.data
}

        