import APIcalls from "./services/APIcalls";
import { GetUserById } from "./User";

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
export const RouteGaurdAdmin = async () =>{
  const userid = JSON.parse(localStorage.getItem('user'));
  if(userid == null){
    return false;
  }
  
  const user = await GetUserById(userid)
  if(user[0].isAdmin){
    return true
  }
  else{
    return false
  }
}

        