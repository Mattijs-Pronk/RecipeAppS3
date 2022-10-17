import bcrypt from "bcryptjs"

export const HashPassword = function(passWord){
    const saltRounds = 10;

    try{
        const HashPassword = bcrypt.hash(passWord, saltRounds);
        HashPassword.toString();
    return HashPassword;
    }
    catch{
        console.log(Error);
    }
}