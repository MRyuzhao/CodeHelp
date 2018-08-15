import { databaseService } from '../services/databaseService';

export default {
  namespace: 'databaseModel',
  state: {
    data: {
      list: [],
      pagination: {},
    },
    tableNameDropDownList: [],
  },

  effects: {
    *fetchTableNameDropDownList({ payload }, { call, put }) {
      const response = yield call(databaseService.dataTables, payload);
      yield put({
        type: 'reducerfetchTableNameDropDownList',
        payload: Array.isArray(response) ? response : [],
      });
    },
    *fetchTableColumns({ payload }, { call, put }) {
      const response = yield call(databaseService.tableColumns, payload);
      yield put({
        type: 'reducerTableColumns',
        payload: Array.isArray(response) ? response : [],
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
        list: action.payload,
      };
    },
  },
};
