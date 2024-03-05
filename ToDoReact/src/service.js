import axios from 'axios';

axios.interceptors.response.use(
  response => {
    return response;
  },
  error => {
    
    console.log('An error occurred:', error);

    return Promise.reject(error);
  }
);

axios.defaults.baseURL = 'http://localhost:5102';

export default {
  getTasks: async () => {
    try {
      const result = await axios.get(`/todos`)
      console.log(result.data)
      return result.data;
    }
    catch (err) {
      console.error( err);
    }

  },

  addTask: async (name) => {
    console.log('addTask', name)
    try {
      const result = await axios.post(`/todos`, {name:name,id:0,isComplete:false})
      return result.data
    }
    catch (err) {
      console.log(err);
    }

  },

  setCompleted: async (id, isComplete) => {
    console.log('setCompleted', { id, isComplete })
    try{
    const result = await axios.put(`/todos/${id}`, isComplete)
      return result.data}
  
      catch(err)  {
        console.log(err);
      }
  },

  deleteTask: async (id) => {
    console.log('deleteTask')
    try{
    const result = await axios.delete(`/todos/${id}`)
     return result.data}
 
     catch(err) {
        console.log(err);
      }

  }
};
