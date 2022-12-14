import APIcalls from "./services/APIcalls";

//functie om in te loggen.
export const Login = async (email, password) => {
  try{
    let response = await APIcalls.Login(email, password)

    localStorage.setItem('user', JSON.stringify((response).data))
    return true
  }
  catch(error){
    console.log(error)
    return false
  } 
}

//functie om te registreren.
export const Register = async (username, email, password) => {
  try{
    await APIcalls.Register(username, password, email)
    return true;
  }
  catch(error){
    console.log(error)
    return false;
  }
}

//functie om een wachtwoord wijziging aan te vragen.
export const ForgotPassword = async (email) => {
  try{
    await APIcalls.ForgotPassword(email)

    return true;
  }
  catch(error){
    console.log(error)
    return false;
  }
}

//functie om het huidige wachtwoord aan te passen.
export const ResetPassword = async (passwordresettoken, password) => {
  try{
    await APIcalls.ResetPassword(passwordresettoken, password)

    return true;
  }
  catch(error){
    console.log(error)
    return false;
  }
}

//functie om een account te verifieren.
export const VerifyAccount = async (email, activateaccounttoken) => {
  try{
    await APIcalls.VerifyAccount(email, activateaccounttoken)

    return true;
  }
  catch(error){
    console.log(error)
    return false;
  }
}

//functie om uit te loggen.
export const Logout = () => {
  const user = JSON.parse(localStorage.getItem('user'))
  if(user !== null){
    localStorage.removeItem('user');
    return true;
  }
  else{
    return false;
  }
}

//functie om te checken of een username al in gebruik is.
export const CheckUser = async (username) => {
  try{
    let response = await APIcalls.CheckUsername(username)
  return response.data
  }
  catch(error){
    console.log(error)
  }
}

//beveiliging van de navigatie naar andere views.
export const RouteGaurd = () =>{
  const user = JSON.parse(localStorage.getItem('user'));
  if(user !== null){
    return true
  }
  else{
    return false
  }
}

        