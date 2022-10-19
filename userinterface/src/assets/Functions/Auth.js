import APIcalls from "./services/APIcalls";
import { HashPassword } from "./PasswordHash";

// export const Login =  (email, password) => {
//   let response = APIcalls.Login(email, password)

//   if(response.status == 404){
//   return false;
//   }
//   else{return true}
// }


export const Register = async (username, email, password) => {
  const isadmin = false;
  const passwordhash = await HashPassword(password);

  await APIcalls.Register(username, passwordhash, email, isadmin)
  //.then(response => {
    //console.log(response);
  //})
}

export const Logout = async () => {
  localStorage.clear();
}

export const CheckUser = async (username) => {
  await APIcalls.CheckUsername(username)
  //.then(response => {
    //console.log(response);
  //})
}

export const CheckEmail = async (email) => {
  await APIcalls.CheckUsername(email)
  //.then(response => {
    //console.log(response);
  //})
}

        