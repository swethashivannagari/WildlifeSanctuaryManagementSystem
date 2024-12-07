export class User {
    username: string;
    passwordHash: string;
    role: string;
    email: string;
  
    constructor(username: string, password: string, role: string, email: string) {
      this.username = username;
      this.passwordHash = password;
      this.role = role;
      this.email = email;
    }
  }