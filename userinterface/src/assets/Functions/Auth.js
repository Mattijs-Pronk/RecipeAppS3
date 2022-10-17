import APIcalls from "./services/APIcalls";

export const Login = async (email, password) => {
    await APIcalls.Login(email, password)
    .then(response => {
      console.log(response);
    })
}