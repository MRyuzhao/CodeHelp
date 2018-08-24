import React, { Component } from 'react';
import { connect } from 'dva';
import styles from './Login.less';
import userManager from "../../utils/userManager"

@connect(({ login }) => ({
  login
}))
export default class LogoutCallbackPage extends Component {
  state = {
    type: 'account',
    autoLogin: true,
  };

  componentDidMount() {
    userManager.signoutRedirect();
  }

  render() {
    return (
      <div className={styles.main}>
        <h3>登出成功，请稍后!</h3>
      </div>
    );
  }
}
