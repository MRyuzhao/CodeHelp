import { WebStorageStateStore, UserManager } from 'oidc-client';

const userManagerConfig = {
  authority: 'http://localhost:5000',//authorization server的地址
  client_id: 'codeHelpClient',
  client_secret: 'secret',
  redirect_uri: 'http://localhost:5002/loginCallback',//登陆成功后跳转回来的地址.必须与identityAuthor一致
  response_type: 'id_token token',
  scope: 'openid profile codeHelpApis',
  post_logout_redirect_uri: 'http://localhost:5002/logoutCallback',//必须与identityAuthor一致
  //silent_redirect_uri: 'http://localhost:5002/database',//自动刷新token的回掉地址
  automaticSilentRenew: true,//启用自动安静刷新token.
  accessTokenExpiringNotificationTime: 10,
  // silentRequestTimeout:10000,
  //userStore: new WebStorageStateStore({ store: window.localStorage })
};

const userManager = new UserManager(userManagerConfig);

export default userManager;