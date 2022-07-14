import { Input } from 'antd';
import { useSiderContext } from 'context/SiderContext';
import { FC } from 'react';

/**
 * Component to set input fields in a Runtime sider.
 */
const RuntimeSiderMapper: FC = () => {
  const { inputParameters, dispatchInput } = useSiderContext();

  return (
    <>
      {inputParameters.map((value: string, index: number) => {
        return (
          <Input
            value={value as unknown as string}
            placeholder="Add parameter"
            onChange={(e) => {
              dispatchInput({ type: 'CHANGE', payload: { e, index } });
            }}
          />
        );
      })}
    </>
  );
};

export default RuntimeSiderMapper;
