import jwt_decode from "jwt-decode";
import {buildAbsoluteUrl, routes} from "../../Routes/Routes";
import axios from "../../Api/axios";
import { AxiosResponse } from "axios";
import {alert} from "../../Components/shared/alert";
import { useHistory } from "react-router-dom";

interface IUserSession {
    accessToken: string;
    login: string;
    id: number;
}

interface IUserSessionJwtData {
    login: string;
    id: number;
}

interface IResultModel {
    token: string;
    returnUrl?: string;
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

    login(data: any, returnUrl?: string) {
        console.log(data);
        axios
            .post<IResultModel>("http://localhost:5000/api/authorize", data)
            .then((res: AxiosResponse<IResultModel>) => {
                const decodedToken = jwt_decode<IUserSessionJwtData>(res.data.token);
                console.log(decodedToken);

                if (!localStorage.getItem(this.localStorageKey)) {
                    localStorage.setItem(
                        this.localStorageKey,
                        JSON.stringify({
                            login: decodedToken.login,
                            id: decodedToken.id,
                            accessToken: res.data.token
                        })
                    );
                }
            })
            .catch(error => {
                alert.error(error);
                console.log(error);
            })
    }

    signIn(token: string) {
        const decodedToken =jwt_decode<IUserSessionJwtData>(token);
    }

    signOut() {
        this.clearSession();

        window.location.href = buildAbsoluteUrl(routes.login);
    }
}

export default new AuthManager();

