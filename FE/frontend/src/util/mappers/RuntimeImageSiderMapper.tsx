import { Card, Image, Input } from 'antd';
import { usePageContext } from 'context/PageContext';
import { useSiderContext } from 'context/SiderContext';
import { FC } from 'react';

/**
 * Component to show selected image .
 */
const RuntimeImageSiderMapper: FC = () => {
  const { inputParameters, setInputParameters, imageParameters } =
    useSiderContext();

  return (
    <>
      {imageParameters.map((value: any, index) => {
        return <Card style={{ width: '100%', padding: 2 }}>{value} </Card>;
      })}
    </>
  );
};

export default RuntimeImageSiderMapper;
