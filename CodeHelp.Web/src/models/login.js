import { routerRedux } from 'dva/router';
import { stringify } from 'qs';
import { fakeAccountLogin } from '../services/api';
import { setUserIdentity, setAuthority } from '../utils/authority';
import { User, UserManager, Log } from 'oidc-client';
import { reloadAuthorized } from '../utils/Authorized';
import { getPageQuery } from '../utils/utils';
import { authenticationService } from '../services/authenticationService';

Log.logger = console;
Log.level = Log.DEBUG;

export default {
  namespace: 'login',

  state: {
    status: undefined,
  },

  effects: {
    *loginCallback({ payload }, { call, put }) {
      //reloadAuthorized();
      // yield put(routerRedux.replace(redirect || '/'));
      debugger
      //const response = yield call(authenticationService.getUser);
      debugger
      // localStorage.setItem("islogin", false);
      // const response = yield call(authenticationService.getUser, payload);
      // yield put({
      //   type: 'changeLoginStatus',
      //   payload: response,
      // });
      yield put(routerRedux.push('/database'));
      // Login successfully
      // if (response) {
      //   //const responseRoles = yield call(functionsService.GetFunctionalRoles);
      //   //setAuthority(responseRoles.role);
      //   //localStorage.setItem("functionRoles", JSON.stringify(responseRoles.functionRoles));
      //   //localStorage.setItem("buttonFunctionAuthorizeds", JSON.stringify(responseRoles.buttonFunctionAuthorizeds));
      //   //reloadAuthorized();

      // }

    },
    *login({ payload }, { call, put }) {
      const response = yield call(fakeAccountLogin, payload);
      yield put({
        type: 'changeLoginStatus',
        payload: response,
      });
      // Login successfully
      if (response.status === 'ok') {

        reloadAuthorized();
        const urlParams = new URL(window.location.href);
        const params = getPageQuery();
        let { redirect } = params;
        if (redirect) {
          const redirectUrlParams = new URL(redirect);
          if (redirectUrlParams.origin === urlParams.origin) {
            redirect = redirect.substr(urlParams.origin.length);
            if (redirect.startsWith('/#')) {
              redirect = redirect.substr(2);
            }
          } else {
            window.location.href = redirect; window.location.href = redirect;
            return;
          }
        }
        yield put(routerRedux.replace(redirect || '/'));
      }
    },
    *logout(_, { put }) {
      yield put({
        type: 'changeLoginStatus',
        payload: {
          status: false,
          currentAuthority: 'guest',
        },
      });
      reloadAuthorized();
      yield put(
        routerRedux.push({
          pathname: '/user/login',
          search: stringify({
            redirect: window.location.href,
          }),
        })
      );
    },
  },

  reducers: {
    changeLoginStatus(state, { payload }) {
      debugger
      setUserIdentity(payload);
      return {
        ...state,
        // status: payload.status,
        // type: payload.type,
      };
    },
  },
};
