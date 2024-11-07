import { useState } from "react";

const useForm = (getFreshModelObject) => {
  const [values, setValues] = useState(getFreshModelObject());
  const [errors, setErrors] = useState({});
  const handleInputChange = (e) => {
    const { username, value } = e.target;
    setValues({
      ...values,
      [username]: value,
    });
  };
  return {
    values,
    setValues,
    errors,
    setErrors,
    handleInputChange,
  };
};

export default useForm;
