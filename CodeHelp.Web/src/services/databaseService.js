import request from '../utils/request';
import { getAPIBaseUrl } from '../common/apibase';
import { stringify } from 'qs';

export class databaseService {
  static async dataTables() {
    return request(`${getAPIBaseUrl()}dataTables`);
  }

  static async birthFile(params) {
    return request(`${getAPIBaseUrl()}dataTables`, {
      method: 'POST',
      body: params,
    });
  }

  static async tableColumns(param) {
    return request(`${getAPIBaseUrl()}tableColumns/pagination?${stringify(param)}`);
  }
}
