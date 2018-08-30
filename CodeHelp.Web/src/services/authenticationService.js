import userManager from "../utils/userManager"

export class authenticationService {
  static async signin() {
    userManager.signinRedirect();
  }

  static async signout() {
    userManager.signoutRedirect();
  }

  static async getUser() {
    return userManager.getUser().then((user) => {
      // avoid page refresh errors
      if (user === null || user === undefined) {
        return userManager.signinRedirectCallback(null);
      }
      return user;
    });
  }

  static async getRenewUser() {
    return userManager.signinSilentCallback(null);
  }

  static async removeUser() {
    userManager.removeUser();
  }
}