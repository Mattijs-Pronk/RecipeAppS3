import APIcalls from "./services/APIcalls";
import { HashPassword } from "./PasswordHash";

// export const Login = async (email, password) => {
//   await APIcalls.Login(email, password)
//   .then(response => {

//   if(response.status == 200){
//   //localStorage.setItem('userId', JSON.stringify(response.data))
//   this.$router.push({name: 'Homepage'})
//   console.log("succes")
//   }
//   })
// }


export const Register = async (username, email, password) => {
  const isadmin = false;
  const passwordhash = await HashPassword(password);

  await APIcalls.Register(username, passwordhash, email, isadmin)
  .then(response => {
    console.log(response);
  })
}

// export default Logout = async () => {

// }