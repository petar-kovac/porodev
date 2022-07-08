import { Card, Image, Input } from 'antd';
import { usePageContext } from 'context/PageContext';
import { FC } from 'react';

/**
 * Component to set input fields in a Runtime sider.
 */
const RuntimeImageSiderMapper: FC = () => {
  const {
    numberOfImages,
    inputParameters,
    setInputParameters,
    imageParameters,
  } = usePageContext();

  const setItems = (e: any, index: any) => {
    const newItem = [...inputParameters];
    newItem[index] = e.target.value;
    setInputParameters(newItem);
  };

  return (
    <>
      {imageParameters.map((value: any, index) => {
        return <Card style={{ width: '100%', padding: 2 }}>{value} </Card>;
      })}
    </>
  );
};

export default RuntimeImageSiderMapper;
