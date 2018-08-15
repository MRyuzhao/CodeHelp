import React, { PureComponent, Fragment } from 'react';
import { connect } from 'dva';
import {
  Row,
  Col,
  Card,
  Form,
  Table,
  Input,
  Select,
  Icon,
  Button,
  Dropdown,
  Menu,
  InputNumber,
  DatePicker,
  Modal,
  message,
  Badge,
  Divider,
} from 'antd';
import StandardTable from 'components/StandardTable';
import PageHeaderLayout from '../../layouts/PageHeaderLayout';
import styles from './database.less';

const FormItem = Form.Item;
const { Option } = Select;
const getValue = obj =>
  Object.keys(obj)
    .map(key => obj[key])
    .join(',');

@connect(({ databaseModel, loading }) => ({
  databaseModel,
  dataLoading: loading.models.databaseModel,
}))
@Form.create()
export default class DatabasePage extends PureComponent {
  state = {

  };

  componentDidMount() {
    this.gettTableNameDropDownList();
  }

  gettTableNameDropDownList = () => {
    const { dispatch } = this.props;
    dispatch({
      type: 'databaseModel/fetchTableNameDropDownList',
    });
  }

  handleStandardTableChange = (pagination, filtersArg, sorter) => {
    const { dispatch } = this.props;
    const { formValues } = this.state;

    const filters = Object.keys(filtersArg).reduce((obj, key) => {
      const newObj = { ...obj };
      newObj[key] = getValue(filtersArg[key]);
      return newObj;
    }, {});

    const params = {
      currentPage: pagination.current,
      pageSize: pagination.pageSize,
      ...formValues,
      ...filters,
    };
    if (sorter.field) {
      params.sorter = `${sorter.field}_${sorter.order}`;
    }

    dispatch({
      type: 'rule/fetch',
      payload: params,
    });
  };

  handleFormReset = () => {
    const { form, dispatch } = this.props;
    form.resetFields();
    this.setState({
      formValues: {},
    });
    dispatch({
      type: 'rule/fetch',
      payload: {},
    });
  };

  handleSearch = e => {
    e.preventDefault();
    const { dispatch, form } = this.props;
    form.validateFields((err, fieldsValue) => {
      if (err) return;
      const values = {
        tableName: fieldsValue.tableName,
      };
      dispatch({
        type: 'databaseModel/fetchTableColumns',
        payload: values,
      });
    });
  };

  renderOptions = list => {
    var a = list.map(x =>
      <Option value={x.value} key={x.value}>{x.text}</Option>
    )
    return a;
  }

  renderForm() {
    const { form, databaseModel: { tableNameDropDownList } } = this.props;
    const { getFieldDecorator } = form;
    return (
      <Form layout="inline">
        <Row gutter={{ md: 8, lg: 24, xl: 48 }}>
          <Col md={8} sm={24}>
            <FormItem label="表名">
              {getFieldDecorator('tableName')(
                <Select
                  showSearch
                  placeholder="请选择表名"
                  optionFilterProp="children"
                  filterOption={(input, option) => option.props.children.toLowerCase().indexOf(input.toLowerCase()) >= 0}
                >
                  {this.renderOptions(tableNameDropDownList)}
                </Select>
              )}
            </FormItem>
          </Col>
          <Col md={8} sm={24}>
            <span className={styles.submitButtons}>
              <Button type="primary" onClick={this.handleSearch}>
                查询
              </Button>
              <Button style={{ marginLeft: 8 }} onClick={this.handleFormReset}>
                重置
              </Button>
            </span>
          </Col>
        </Row>
      </Form>
    );
  }

  render() {
    const {
      databaseModel: { data: { list } },
      dataLoading,
    } = this.props;

    const columns = [
      {
        title: '表名',
        dataIndex: 'tableName',
      },
      {
        title: '列名',
        dataIndex: 'columnName',
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

    return (
      <PageHeaderLayout title="数据库">
        <Card bordered={false}>
          <div className={styles.tableList}>
            <div className={styles.tableListForm}>{this.renderForm()}</div>
            <Table
              loading={dataLoading}
              dataSource={list}
              columns={columns}
              onChange={this.handleStandardTableChange}
            />
          </div>
        </Card>
      </PageHeaderLayout>
    );
  }
}
