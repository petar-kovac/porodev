import { Image, Input } from 'antd';
import { usePageContext } from 'context/PageContext';
import { FC } from 'react';

/**
 * Component to set input fields in a Runtime sider.
 */
const RuntimeImageSiderMapper: FC = () => {
  const { numberOfImages, inputParameters, setInputParameters } =
    usePageContext();

  const setItems = (e: any, index: any) => {
    const newItem = [...inputParameters];
    newItem[index] = e.target.value;
    setInputParameters(newItem);
  };

  return (
    <>
      {[...Array(2)].map((value, index) => {
        return (
          <Image
            width={50}
            src="https://zos.alipayobjects.com/rmsportal/jkjgkEfvpUPVyRjUImniVslZfWPnJuuZ.png"
          />
        );
      })}
    </>
  );
};

export default RuntimeImageSiderMapper;
