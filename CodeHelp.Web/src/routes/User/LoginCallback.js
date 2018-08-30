import React, { Component } from 'react';
import { connect } from 'dva';
import styles from './Login.less';

@connect(({ login }) => ({
  login,
}))
export default class LoginCallbackPage extends Component {

  componentDidMount() {
    this.props.dispatch({
      type: 'login/loginCallback',
    });
  }

  render() {
    return (
      <div className={styles.main}>
        <h3>登录成功，将跳转到主页面!</h3>
      </div>
    );
  }
}
