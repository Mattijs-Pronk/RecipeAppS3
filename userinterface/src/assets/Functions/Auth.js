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
  try{
    await APIcalls.Register(username, password, email, isadmin)
    return true;
  }
  catch{
    return false;
  }
}

export const ForgotPassword = async (email) => {
  try{
    await APIcalls.ForgotPassword(email)

    return true;
  }
  catch{
    return false;
  }
}

export const ResetPassword = async (passwordresettoken, password) => {
  try{
    await APIcalls.ResetPassword(passwordresettoken, password)

    return true;
  }
  catch{
    return false;
  }
}

export const VerifyAccount = async (email, activateaccounttoken) => {
  try{
    await APIcalls.VerifyAccount(email, activateaccounttoken)

    return true;
  }
  catch{
    return false;
  }
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

        