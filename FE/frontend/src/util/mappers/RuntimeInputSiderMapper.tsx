import { Input } from 'antd';
import { usePageContext } from 'context/PageContext';
import { useSiderContext } from 'context/SiderContext';
import { FC } from 'react';

/**
 * Component to set input fields in a Runtime sider.
 */
const RuntimeSiderMapper: FC = () => {
  const { inputParameters, setInputParameters } = useSiderContext();

  const setItems = (e: any, index: any) => {
    const newItem = [...inputParameters];
    newItem[index] = e.target.value;
    setInputParameters(newItem);
  };

  return (
    <>
      {inputParameters.map((value, index) => {
        return (
          <Input
            value={inputParameters[index] as unknown as string}
            placeholder="Add parameter"
            onChange={(e) => {
              setItems(e, index);
            }}
          />
        );
      })}
    </>
  );
};

export default RuntimeSiderMapper;
