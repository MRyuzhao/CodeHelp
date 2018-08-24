import React, { PureComponent } from 'react';
import { connect } from 'dva';
import { Row, Col, Card, Form, Table, Select, Button, Modal, Input, Icon, message } from 'antd';
import PageHeaderLayout from '../../layouts/PageHeaderLayout';
import styles from './database.less';

const FormItem = Form.Item;
const { Option } = Select;
message.config({
  duration: 3,
  maxCount: 2,
});

@Form.create()
export class PageModal extends PureComponent {
  state = {};

  okHandle = e => {
    e.preventDefault();
    const { form, handleBirthFile } = this.props;
    form.validateFields((err, fieldsValue) => {
      if (err) return;
      handleBirthFile(fieldsValue.address);
    });
  };

  handleeEptyAddress = () => {
    this.props.form.setFieldsValue({
      address: '',
    });
  };

  render() {
    const { form, modalVisible, handleModalVisible } = this.props;
    const suffix = form.getFieldValue('address') ? (
      <Icon type="close-circle" onClick={this.handleeEptyAddress} />
    ) : null;

    return (
      <Modal
        title="生成文件"
        visible={modalVisible}
        onOk={this.okHandle}
        onCancel={() => handleModalVisible(false)}
        width={800}
      >
        <FormItem labelCol={{ span: 2 }} wrapperCol={{ span: 22 }} label="路径">
          {form.getFieldDecorator('address', {
            rules: [{ required: true, message: '请输入生成文件所在的路径' }],
          })(
            <Input
              placeholder="请输入生成文件所在的路径"
              prefix={<Icon type="link" style={{ color: 'rgba(0,0,0,.25)' }} />}
              suffix={suffix}
            />
          )}
        </FormItem>
      </Modal>
    );
  }
}

@connect(({ databaseModel, loading }) => ({
  databaseModel,
  dataLoading: loading.models.databaseModel,
}))
@Form.create()
export default class DatabasePage extends PureComponent {
  state = {
    pagination: {
      currentPage: 1,
      pageSize: 10,
      orderByPropertyName: 'columnColid',
      isAsc: false,
    },
    queryObject: {},
    modalVisible: false,
  };

  componentDidMount() {
    this.gettTableNameDropDownList();
    this.getDatas();
  }

  gettTableNameDropDownList = () => {
    const { dispatch } = this.props;
    dispatch({
      type: 'databaseModel/fetchTableNameDropDownList',
    });
  };

  getDatas = () => {
    const queryObject = this.state.queryObject;
    const page = this.state.pagination;
    const queryString = { ...queryObject, ...page };
    this.props.dispatch({
      type: 'databaseModel/fetchTableColumns',
      payload: queryString,
    });
  };

  handleBirthCode = e => {
    e.preventDefault();
    const { form } = this.props;
    form.validateFields((_, fieldsValue) => {
      if (!fieldsValue || !fieldsValue.tableName) {
        message.warning('请选择数据库表');
        return;
      }
      this.handleModalVisible(true);
    });
  };

  handleSearch = e => {
    e.preventDefault();
    const { form } = this.props;
    form.validateFields((err, fieldsValue) => {
      if (err) return;
      this.setState(
        {
          queryObject: {
            tableName: fieldsValue.tableName,
          },
        },
        () => this.getDatas()
      );
    });
  };

  handleFormReset = () => {
    const { form } = this.props;
    form.resetFields();
    this.setState(
      {
        queryObject: {},
      },
      () => this.getDatas()
    );
  };

  handleTableChange = (pagination, _, sorter) => {
    this.setState(
      {
        pagination: {
          pageSize: pagination.pageSize,
          currentPage: pagination.current,
          orderByPropertyName: sorter.field,
          isAsc: sorter.order && sorter.order === 'ascend',
        },
      },
      () => this.getDatas()
    );
  };

  handleBirthFile = address => {
    const { form, dispatch } = this.props;
    dispatch({
      type: 'databaseModel/birthFile',
      payload: {
        BirthPath: address,
        TableName: form.getFieldValue('tableName'),
      },
    });
  };

  handleQueryObject = fieldsValue => {
    this.setState({
      queryObject: {
        vehicleNo: fieldsValue.vehicleNo,
        carNumber: fieldsValue.carNumber,
      },
    });
  };

  handleModalVisible = value => {
    this.setState({
      modalVisible: value,
    });
  };

  renderOptions = list => {
    return list.map(x => (
      <Option value={x.value} key={x.value}>
        {x.text}
      </Option>
    ));
  };

  renderForm() {
    const {
      form,
      databaseModel: { tableNameDropDownList },
    } = this.props;
    const { getFieldDecorator } = form;
    return (
      <Form layout="inline">
        <Row gutter={{ md: 8, lg: 24, xl: 48 }}>
          <Col md={16} sm={24}>
            <FormItem label="表名">
              {getFieldDecorator('tableName')(
                <Select
                  showSearch
                  placeholder="请选择表名"
                  optionFilterProp="children"
                  filterOption={(input, option) =>
                    option.props.children.toLowerCase().indexOf(input.toLowerCase()) >= 0
                  }
                >
                  {this.renderOptions(tableNameDropDownList)}
                </Select>
              )}
            </FormItem>
          </Col>
        </Row>
      </Form>
    );
  }

  renderButton() {
    return (
      <div className={styles.tableListOperator}>
        <div className={styles.buttonFloatLeft}>
          <Button icon="plus" type="primary" onClick={this.handleBirthCode}>
            生成
          </Button>
        </div>
        <div className={styles.buttonFloatRight}>
          <Button icon="reload" onClick={this.handleFormReset}>
            重置
          </Button>
          <Button icon="search" type="primary" onClick={this.handleSearch}>
            查询
          </Button>
          <div className={styles.clear} />
        </div>
      </div>
    );
  }

  render() {
    const {
      databaseModel: {
        data: { list, pagination },
      },
      dataLoading,
    } = this.props;
    const columns = [
      {
        title: '序号',
        width: 100,
        dataIndex: 'columnColid',
        fixed: 'left',
        sorter: true,
      },
      {
        title: '列名',
        width: 250,
        dataIndex: 'columnName',
        fixed: 'left',
      },
      {
        title: '表名',
        dataIndex: 'tableName',
      },
      {
        title: '描述',
        dataIndex: 'description',
      },
      {
        title: '类型',
        dataIndex: 'columnType',
      },
      {
        title: '长度',
        dataIndex: 'columnLength',
      },
      {
        title: '默认值',
        dataIndex: 'defaultValue',
      },
      {
        title: '可空',
        dataIndex: 'isNull',
      },
      {
        title: '主键',
        dataIndex: 'isPrimaryKey',
      },
      {
        title: '自增',
        dataIndex: 'isIdentity',
      },
      {
        title: '小数位数',
        dataIndex: 'scale',
      },
    ];
    const newPagination = {
      ...pagination,
      showSizeChanger: true,
      showQuickJumper: true,
      showTotal: total => `总共 ${total} 条记录`,
    };
    const parentDatas = {
      modalVisible: this.state.modalVisible,
    };
    const parentMethods = {
      handleModalVisible: value => this.handleModalVisible(value),
      handleBirthFile: this.handleBirthFile.bind(this),
    };

    return (
      <PageHeaderLayout title="DataBase">
        <Card bordered={false}>
          <div className={styles.tableList}>
            <div className={styles.tableListForm}>{this.renderForm()}</div>
            {this.renderButton()}
            <Table
              loading={dataLoading}
              dataSource={list}
              pagination={newPagination}
              columns={columns}
              onChange={this.handleTableChange}
              scroll={{ x: 1500 }}
            />
          </div>
        </Card>
        <PageModal {...parentDatas} {...parentMethods} />
      </PageHeaderLayout>
    );
  }
}
