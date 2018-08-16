import { databaseService } from '../services/databaseService';

export default {
  namespace: 'databaseModel',
  state: {
    data: {
      list: [],
    },
    tableNameDropDownList: [],
  },

  effects: {
    *fetchTableNameDropDownList(_, { call, put }) {
      const response = yield call(databaseService.dataTables);
      yield put({
        type: 'reducerTableNameDropDownList',
        payload: response,
      });
    },
    *fetchTableColumns({ payload }, { call, put }) {
      const response = yield call(databaseService.tableColumns, payload.tableName);
      yield put({
        type: 'reducerTableColumns',
        payload: response,
      });
    },
  },

  reducers: {
    reducerTableNameDropDownList(state, action) {
      return {
        ...state,
        tableNameDropDownList: action.payload,
      };
    },
    reducerTableColumns(state, action) {
      return {
        ...state,
        data: { ...action.payload },
      };
    },
  },
};
