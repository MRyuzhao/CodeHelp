// use localStorage to store the authority info, which might be sent from server in actual project.
export function getAuthority() {
  // return localStorage.getItem('antd-pro-authority') || ['admin', 'user'];
  return localStorage.getItem('antd-pro-authority');
}

export function setAuthority(authority) {
  return localStorage.setItem('antd-pro-authority', authority);
}

export function getUserIdentity() {
  const identity=localStorage.getItem('antd-pro-user-identity');
  return JSON.parse(identity)
}

export function setUserIdentity(user) {
  return localStorage.setItem('antd-pro-user-identity', JSON.stringify(user));
}
