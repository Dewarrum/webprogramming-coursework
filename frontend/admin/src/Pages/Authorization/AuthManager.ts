import _ from "lodash";
import jwt_decode from "jwt-decode";

interface IUserSession {
    accessToken: string;
    login: string;
    email: string;
}

interface IUserSessionJwtData {
    userLogin: string;
    userEmail: string;
}

export class AuthManager {
    private readonly localStorageKey = "authUserSession";

    get userSession(): IUserSession {
        const userSessionJson = localStorage.getItem(this.localStorageKey);
        return userSessionJson ? JSON.parse(userSessionJson) : null;
    }

    clearSession() {
        localStorage.removeItem(this.localStorageKey);
    }

    login(returnUrl?: string) {
        const signInUrl = ``
    }
}

export default new AuthManager();

