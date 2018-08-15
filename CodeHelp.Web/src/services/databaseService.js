import request from '../utils/request';

export class databaseService {
  static async dataTables(tableName) {
    return request(`/api/dataTables/${tableName}`);
  }

  static async tableColumns(tableName) {
    return request(`/api/tableColumns/${tableName}`);
  }
}
